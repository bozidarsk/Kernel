bits 32

global _start
extern _start64
extern gdt64
extern gdt64.code_segment
extern gdt64.descriptor
extern multiboot_address

section .bss

align 4096
page_table_l4: resb 4096
page_table_l3: resb 4096
page_table_l2: resb 4096

align 16
resb 16384 ; 8192
stack:

section .text

_start:
	cli

	mov dword [multiboot_address], ebx
	mov esp, stack

	call enable_a20
	jmp enter_long_mode

	jmp error

enable_a20:
	mov edi, 0x112345 ; odd megabyte address
	mov esi, 0x012345 ; even megabyte address
	mov [esi], esi ; making sure that both addresses contain diffrent values
	mov [edi], edi ; (if A20 line is cleared the two pointers would point to the address 0x012345 that would contain 0x112345 (edi)) 
	cmpsd ; compare addresses to see if the're equivalent
	je error

	ret

setup_page_tables:
	mov eax, page_table_l3 
	or eax, 0b11
	mov [page_table_l4], eax

	mov eax, page_table_l2 
	or eax, 0b11
	mov [page_table_l3], eax

	mov ecx, 0
	.loop: ; ecx = 0; ecx <= 512; ecx++
	mov eax, 0x200000
	mul ecx
	or eax, 0b10000011
	mov [page_table_l2 + ecx * 8], eax
	inc ecx
	cmp ecx, 512
	jne .loop

	ret

enter_long_mode:
	; check for long mode
	mov eax, 0x80000001
	cpuid
	and edx, 1 << 29
	cmp edx, 0
	je error

	; setup page tables
	call setup_page_tables

	; pass table pointer to cpu
	mov eax, page_table_l4
	mov cr3, eax

	; enable PAE
	mov eax, cr4
	or eax, 1 << 5
	mov cr4, eax

	; enable long mode
	mov ecx, 0xc0000080
	rdmsr
	or eax, 1 << 8
	wrmsr

	; enable paging
	mov eax, cr0
	or eax, 1 << 31
	mov cr0, eax

	lgdt [gdt64.descriptor]
	jmp gdt64.code_segment:_start64

error:
	mov dword [0xb8000], 0x04720465
	mov dword [0xb8004], 0x00000472
	cli
	hlt
